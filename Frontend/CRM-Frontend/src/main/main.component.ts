import { Component, OnInit } from '@angular/core';
import { BackendService, Customer } from '../backend.service';
import { FormComponent } from '../form/form.component';
import { TableComponent } from '../table/table.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  standalone: true,
  imports: [CommonModule, FormComponent, TableComponent],
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  title = 'Customer Invoice CRM';
  displayColumns = ['Name', 'Phone Number', 'Address'];
  columns: (keyof Customer)[] = ['name', 'phoneNum', 'address'];
  data: Customer[] = [];

  constructor(private backendService: BackendService) {}

  handleCustomerAdded(customer: Customer) {
    this.data.push(customer);
  }

  handleCustomerDeleted(id: string) {
    this.backendService.deleteCustomer(id).subscribe(() => {
      this.data = this.data.filter(customer => customer.id !== id);
    });
  }

  ngOnInit(): void {
    this.backendService.getCustomers().subscribe((customers: Customer[]) => {
      this.data = customers;
    });
  }
}
