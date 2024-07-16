// src/app/form.component.ts
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgIf } from '@angular/common';
import { HttpClient } from "@angular/common/http";
import { Customer } from '../backend.service';

@Component({
  selector: 'app-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf
  ],
  templateUrl: './form.component.html',
  styleUrl: './form.component.css'
})
export class FormComponent implements OnInit {
  @Output() customerAdded = new EventEmitter<Customer>();

  group: FormGroup = this.fb.group({
    name: this.fb.control('', [Validators.required]),
    phoneNum: this.fb.control('', [Validators.required]), // Ensure this matches backend
    address: this.fb.control('', [Validators.required]),
    id: this.fb.control('', [Validators.required]),
  });

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  ngOnInit() {}

  submit() {
    const customer: Customer = {
      id: '', // This will be set by the server
      name: this.group.get('name')?.value ?? '',
      phoneNum: this.group.get('phoneNum')?.value ?? '',
      address: this.group.get('address')?.value ?? '',
    };

    this.http.post<Customer>("https://localhost:7197/api/customers/", customer).subscribe(
      (createdCustomer) => {
        console.log('Customer created:', createdCustomer);
        this.customerAdded.emit(createdCustomer); // Emit the customer returned by the server
        this.group.reset();
      },
      (error) => {
        console.error('Error creating customer:', error);
      }
    );
  }


}
