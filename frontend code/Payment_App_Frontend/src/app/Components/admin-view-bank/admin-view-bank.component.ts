// import { Component, OnInit } from '@angular/core';
// import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';
// import { BankService } from '../../Services/bank.service';
// import { Bank } from '../../Models/Bank';
// import { BankDTO } from '../../DTO/BankDTO';
// import { RouterLink } from '@angular/router';
// import { NameFilterComponent } from '../Filters/name-filter/name-filter.component';
// import { BooleanFilterComponent } from '../Filters/boolean-filter/boolean-filter.component';
// import { NotificationService } from '../../Services/notification.service';

// @Component({
//   selector: 'app-admin-view-bank',
//   standalone: true,
//   imports: [CommonModule, FormsModule, RouterLink, NameFilterComponent, BooleanFilterComponent],
//   templateUrl: './admin-view-bank.component.html',
//   styleUrls: ['./admin-view-bank.component.css']
// })
// export class AdminViewBankComponent implements OnInit {
//   banks: Bank[] = [];
//   filters: any = {}
//   selectedBank: Bank | null = null;
//   bankDto: BankDTO = { bankName: '', ifsc: '' };
//   loading = true;
//   responseMessage: string | null = null;

//   statusOptions = [
//     { id: true, name: "Active" },
//     { id: false, name: "InActive" }
//   ];

//   constructor(private bankService: BankService, private notify: NotificationService ) { }

//   ngOnInit(): void {
//     const params = new URLSearchParams(this.filters).toString();
//     this.fetchBanks(params);
//   }

//   fetchBanks(params: string) {
//     this.loading = true;
//     this.bankService.getAllBanks(params).subscribe({
//       next: (res) => {
//         console.log(res);
//         this.banks = res;
//         this.loading = false;
//       },
//       error: () => {
//         this.loading = false;
//         this.responseMessage = "Failed to fetch banks!";
//       }
//     });
//   }



//   editBank(bank: Bank) {
//     this.selectedBank = bank;
//     this.bankDto = { bankName: bank.bankName, ifsc: bank.ifsc };
//   }

//   updateBank() {
//     if (!this.selectedBank) return;

//     this.bankService.updateBank(this.selectedBank.bankId, this.bankDto).subscribe({
//       next: () => {
//         this.responseMessage = "Bank updated successfully!";
//         this.selectedBank = null;
//         const params = new URLSearchParams(this.filters).toString();
//         this.fetchBanks(params);
//       },
//       error: () => this.responseMessage = "Failed to update bank!"
//     });
//   }

//   deleteBank(id: number) {
//     if (confirm("Are you sure you want to delete this bank?")) {
//       this.bankService.deleteBank(id).subscribe({
//         next: () => {
//           this.responseMessage = "Bank deleted successfully!";
//           const params = new URLSearchParams(this.filters).toString();
//           this.fetchBanks(params);
//         },
//         error: () => this.responseMessage = "Failed to delete bank!"
//       });
//     }
//   }

//   onNameFilter(name: { payerName: string }) {
//     this.filters.bankName = name.payerName;

//     const params = new URLSearchParams(this.filters).toString();
//     this.fetchBanks(params);
//   }

//   onBooleanFilter(filter: { isActive: boolean | null }) {
//     if (filter.isActive !== null) {
//       this.filters.isActive = filter.isActive;
//     } else {
//       delete this.filters.isActive;
//     }

//     const params = new URLSearchParams(this.filters).toString();
//     this.fetchBanks(params);
//   }
// }

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BankService } from '../../Services/bank.service';
import { Bank } from '../../Models/Bank';
import { BankDTO } from '../../DTO/BankDTO';
import { RouterLink } from '@angular/router';
import { NameFilterComponent } from '../Filters/name-filter/name-filter.component';
import { BooleanFilterComponent } from '../Filters/boolean-filter/boolean-filter.component';
import { NotificationService } from '../../Services/notification.service';

@Component({
  selector: 'app-admin-view-bank',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, NameFilterComponent, BooleanFilterComponent],
  templateUrl: './admin-view-bank.component.html',
  styleUrls: ['./admin-view-bank.component.css']
})
export class AdminViewBankComponent implements OnInit {
  banks: Bank[] = []; // This holds the *paged* list
  allBanks: Bank[] = []; // This holds the *full* list from the server
  filters: any = {}
  selectedBank: Bank | null = null;
  bankDto: BankDTO = { bankName: '', ifsc: '' };
  loading = true;
  responseMessage: string | null = null;

  // Pagination properties
  pageNumber = 1;
  pageSize = 5; // You can change this to 10 or 20
  totalRecords = 0;

  statusOptions = [
    { id: true, name: "Active" },
    { id: false, name: "InActive" }
  ];

  constructor(private bankService: BankService, private notify: NotificationService ) { }

  ngOnInit(): void {
    const params = new URLSearchParams(this.filters).toString();
    this.fetchBanks(params);
  }

  fetchBanks(params: string) {
    this.loading = true;
    // This service call fetches the *filtered* list based on params
    this.bankService.getAllBanks(params).subscribe({
      next: (res) => {
        this.allBanks = res; // Store the full filtered list
        this.totalRecords = res.length;
        this.pageNumber = 1; // Reset to page 1 after every filter
        this.updatePagedBanks(); // Slice for the current page
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this.responseMessage = "Failed to fetch banks!";
        this.notify.error("Failed to fetch banks!");
      }
    });
  }

  /** Slices the full list (allBanks) to get the paged list (banks) */
  updatePagedBanks() {
    const startIndex = (this.pageNumber - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.banks = this.allBanks.slice(startIndex, endIndex);
  }

  // --- Pagination Methods ---
  goToPage(page: number): void {
    const newPage = Math.max(1, Math.min(page, this.totalPages));
    if (newPage === this.pageNumber) return;

    this.pageNumber = newPage;
    this.updatePagedBanks();
    //
    // BUG FIX: Removed the call to fetchBanks() here.
    // We already have the data in allBanks, we just need to re-slice it.
    //
  }

  get totalPages(): number {
    if (this.totalRecords === 0) return 1;
    return Math.ceil(this.totalRecords / this.pageSize);
  }

  get pages(): number[] {
    const totalPages = this.totalPages;
    if (totalPages === 0) return [];

    let start = Math.max(1, this.pageNumber - 2);
    let end = Math.min(totalPages, this.pageNumber + 2);
    
    // Adjust boundaries to ensure 5 pages are shown if possible
    if (end - start < 4) {
        if (start === 1) end = Math.min(totalPages, start + 4);
        if (end === totalPages) start = Math.max(1, end - 4);
    }
    
    const pageArray = [];
    for (let i = start; i <= end; i++) {
      pageArray.push(i);
    }
    return pageArray;
  }

  editBank(bank: Bank) {
    this.selectedBank = bank;
    this.bankDto = { bankName: bank.bankName, ifsc: bank.ifsc };
  }

  updateBank() {
    if (!this.selectedBank) return;

    this.bankService.updateBank(this.selectedBank.bankId, this.bankDto).subscribe({
      next: () => {
        this.notify.success("Bank updated successfully!");
        this.selectedBank = null;
        const params = new URLSearchParams(this.filters).toString();
        this.fetchBanks(params);
      },
      error: () => this.notify.error("Failed to update bank!")
    });
  }

  deleteBank(id: number) {
    // Note: Using a custom modal is better than confirm()
    this.notify.warning("Delete functionality should use a custom modal instead of 'confirm()'."); 
    
    if (confirm("Are you sure you want to delete this bank?")) {
      this.bankService.deleteBank(id).subscribe({
        next: () => {
          this.notify.success("Bank deleted successfully!");
          const params = new URLSearchParams(this.filters).toString();
          this.fetchBanks(params);
        },
        error: () => this.notify.error("Failed to delete bank!")
      });
    }
  }

  onNameFilter(name: { payerName: string }) {
    this.filters.bankName = name.payerName;
    const params = new URLSearchParams(this.filters).toString();
    this.fetchBanks(params);
  }

  onBooleanFilter(filter: { isActive: boolean | null }) {
    if (filter.isActive !== null) {
      this.filters.isActive = filter.isActive;
    } else {
      delete this.filters.isActive;
    }
    const params = new URLSearchParams(this.filters).toString();
    this.fetchBanks(params);
  }
}