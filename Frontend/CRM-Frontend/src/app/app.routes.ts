import { Routes } from '@angular/router';
import { MainComponent } from '../main/main.component';
import { CustomerInvoicesComponent } from '../customer-invoices/customer-invoices.component';
import { InvoiceItemsComponent } from '../invoice-items/invoice-items.component';

export const routes: Routes = [
  { path: '', component: MainComponent },
  { path: 'customer/:id', component: CustomerInvoicesComponent },
  { path: 'invoice/:id', component: InvoiceItemsComponent } // New route for invoice items
];
