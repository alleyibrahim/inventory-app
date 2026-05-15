import { Component, EventEmitter, Input, OnInit, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Product, ProductPayload } from '../../models/product.model';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.scss'
})
export class ProductFormComponent implements OnInit {
  @Input() product: Product | null = null;
  @Input() error: string | null = null;
  @Output() submitted = new EventEmitter<ProductPayload>();
  @Output() cancelled = new EventEmitter<void>();

  private readonly fb = inject(FormBuilder);

  readonly categories = ['Electronics', 'Clothing', 'Food', 'Tools', 'Other'];

  form = this.fb.group({
    name: ['', Validators.required],
    sku: ['', Validators.required],
    price: [0, [Validators.required, Validators.min(0.01)]],
    stockQuantity: [0, [Validators.required, Validators.min(0)]],
    category: [this.categories[0], Validators.required]
  });

  ngOnInit(): void {
    if (this.product) {
      this.form.patchValue(this.product);
    }
  }

  submit(): void {
    if (this.form.invalid) return;
    this.submitted.emit(this.form.value as ProductPayload);
  }
}
