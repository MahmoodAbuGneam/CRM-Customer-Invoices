import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { BackendService, Customer, Invoice } from '../backend.service';
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
    console.log('CustomerInvoicesComponent initialized');
    this.route.paramMap.subscribe(params => {
      console.log('Route params:', params);
      this.customerId = params.get('id');
      console.log('Extracted customerId:', this.customerId);
      if (this.customerId) {
        this.loadCustomerData(this.customerId);
      } else {
        console.error('Customer ID not provided');
        // Navigate back to the main page
        // Inject Router in the constructor and use it here
        // this.router.navigate(['/']);
      }
    });
  }

  loadCustomerData(customerId: string): void {
    this.backendService.getCustomer(customerId).subscribe(
      (customer: Customer) => {
        console.log('Loaded customer:', customer);
        this.customerName = customer.name;
        this.loadInvoices(customerId);
      },
      error => {
        console.error('Error loading customer:', error);
        // Handle error (e.g., show error message, navigate to error page)
      }
    );
  }

  loadInvoices(customerId: string): void {
    this.backendService.getInvoicesByCustomerId(customerId).subscribe(
      (invoices: Invoice[]) => {
        this.invoices = invoices;
      },
      error => {
        console.error('Error loading invoices:', error);
        // Handle error (e.g., show error message)
      }
    );
  }

  createInvoice(): void {
    if (this.customerId) {
      const newInvoice: Partial<Invoice> = {
        customer_id: this.customerId,
        date: new Date()
      };
      this.backendService.addInvoice(newInvoice).subscribe(
        (invoice: Invoice) => {
          this.invoices.push(invoice);
        },
        error => {
          console.error('Error creating invoice:', error);
          // Handle error (e.g., show error message)
        }
      );
    }
  }

  deleteInvoice(id: string): void {
    this.backendService.deleteInvoice(id).subscribe(
      () => {
        this.invoices = this.invoices.filter(invoice => invoice.id !== id);
      },
      error => {
        console.error('Error deleting invoice:', error);
        // Handle error (e.g., show error message)
      }
    );
  }
}
