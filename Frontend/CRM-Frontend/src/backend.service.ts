import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

export interface Customer {
  id: string;
  name: string;
  phoneNum: string;
  address: string;
}

export interface Invoice {
  id: string;
  customer_id: string;
  date: Date;
}

export interface Item {
  id: string;
  invoice_id: string;
  name: string;
  quantity: number;
  price: number;
}

@Injectable({
  providedIn: 'root'
})
export class BackendService {
  private apiUrl = 'https://localhost:7197/api';

  constructor(private httpClient: HttpClient) { }

  addCustomer(customer: Customer): Observable<Customer> {
    return this.httpClient.post<Customer>(`${this.apiUrl}/customers`, customer);
  }

  getCustomers(): Observable<Customer[]> {
    return this.httpClient.get<Customer[]>(`${this.apiUrl}/customers`);
  }

  getCustomer(id: string): Observable<Customer> {
    return this.httpClient.get<Customer>(`${this.apiUrl}/customers/${id}`);
  }

  deleteCustomer(id: string): Observable<void> {
    return this.httpClient.delete<void>(`${this.apiUrl}/customers/${id}`);
  }

  getInvoicesByCustomerId(customerId: string): Observable<Invoice[]> {
    return this.httpClient.get<Invoice[]>(`${this.apiUrl}/invoices/customer/${customerId}`);
  }

  addInvoice(invoice: Partial<Invoice>): Observable<Invoice> {
    return this.httpClient.post<Invoice>(`${this.apiUrl}/invoices`, invoice);
  }

  deleteInvoice(id: string): Observable<void> {
    return this.httpClient.delete<void>(`${this.apiUrl}/invoices/${id}`);
  }

  getItemsByInvoiceId(invoiceId: string): Observable<Item[]> {
    return this.httpClient.get<Item[]>(`${this.apiUrl}/items/invoice/${invoiceId}`);
  }

  addItem(item: Partial<Item>): Observable<Item> {
    return this.httpClient.post<Item>(`${this.apiUrl}/items`, item);
  }

  deleteItem(id: string): Observable<void> {
    return this.httpClient.delete<void>(`${this.apiUrl}/items/${id}`);
  }
}
