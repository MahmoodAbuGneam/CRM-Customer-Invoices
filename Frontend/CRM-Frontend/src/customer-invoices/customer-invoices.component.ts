import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {BackendService, Customer, Invoice} from '../backend.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-customer-invoices',
  templateUrl: './customer-invoices.component.html',
  standalone: true,
  imports: [CommonModule, RouterLink],
  styleUrls: ['./customer-invoices.component.css']
})
export class CustomerInvoicesComponent implements OnInit {
  customerId: string | null = null;
  customerName: string | null = null;
  invoices: Invoice[] = [];

  constructor(private route: ActivatedRoute, private backendService: BackendService) {}

  ngOnInit(): void {
    this.customerId = this.route.snapshot.paramMap.get('id');
    if (this.customerId) {
      this.backendService.getCustomer(this.customerId).subscribe((customer: Customer) => {
        this.customerName = customer.name;
      });
      this.backendService.getInvoicesByCustomerId(this.customerId).subscribe((invoices: Invoice[]) => {
        this.invoices = invoices;
      });
    }
  }

  createInvoice(): void {
    if (this.customerId) {
      const newInvoice: Partial<Invoice> = {
        customer_id: this.customerId,
        date: new Date() // Ensure date is set to the current date and time
      };
      this.backendService.addInvoice(newInvoice).subscribe((invoice: Invoice) => {
        this.invoices.push(invoice);
      });
    }
  }

  deleteInvoice(id: string): void {
    this.backendService.deleteInvoice(id).subscribe(
      () => {
        this.invoices = this.invoices.filter(invoice => invoice.id !== id);
      },
      error => {
        console.error('Error deleting invoice:', error);
      }
    );
  }
}
