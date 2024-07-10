import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterLink} from "@angular/router";

interface Customer {
  id: string;
  name: string;
  phoneNum: string;
  address: string;
}

@Component({
  selector: 'app-table',
  standalone: true,
  imports: [CommonModule, RouterLink], // Add CommonModule here
  templateUrl: './table.component.html',
  styleUrl: './table.component.css'
})
export class TableComponent {
  @Input() title = '';
  @Input() displayColumns: string[] = ['Name', 'Phone Number', 'Address'];
  @Input() columns: (keyof Customer)[] = ['name', 'phoneNum', 'address'];
  @Input() data: Customer[] = [];
  @Output() customerDeleted = new EventEmitter<string>();

  deleteCustomer(id: string) {
    this.customerDeleted.emit(id);
  }
}
