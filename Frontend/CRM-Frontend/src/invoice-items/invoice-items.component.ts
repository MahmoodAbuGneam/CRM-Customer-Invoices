import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BackendService, Item } from '../backend.service';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-invoice-items',
  templateUrl: './invoice-items.component.html',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  styleUrls: ['./invoice-items.component.css']
})
export class InvoiceItemsComponent implements OnInit {
  invoiceId: string | null = null;
  items: Item[] = [];
  itemForm: FormGroup;

  constructor(private route: ActivatedRoute, private backendService: BackendService, private fb: FormBuilder) {
    this.itemForm = this.fb.group({
      name: [''],
      quantity: [1],
      price: [0]
    });
  }

  ngOnInit(): void {
    this.invoiceId = this.route.snapshot.paramMap.get('id');
    if (this.invoiceId) {
      this.backendService.getItemsByInvoiceId(this.invoiceId).subscribe((items: Item[]) => {
        this.items = items;
      });
    }
  }

  createItem(): void {
    if (this.invoiceId && this.itemForm.valid) {
      const newItem: Partial<Item> = {
        invoice_id: this.invoiceId,
        name: this.itemForm.value.name,
        quantity: this.itemForm.value.quantity,
        price: this.itemForm.value.price
      };
      this.backendService.addItem(newItem).subscribe((item: Item) => {
        this.items.push(item);
        this.itemForm.reset({ name: '', quantity: 1, price: 0 });
      });
    }
  }

  deleteItem(id: string): void {
    this.backendService.deleteItem(id).subscribe(
      () => {
        this.items = this.items.filter(item => item.id !== id);
      },
      error => {
        console.error('Error deleting item:', error);
      }
    );
  }
}
