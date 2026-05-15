import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../services/product.service';
import { ProductFormComponent } from '../product-form/product-form.component';
import { Product, ProductPayload } from '../../models/product.model';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, ProductFormComponent],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss'
})
export class ProductListComponent implements OnInit {
  private readonly productService = inject(ProductService);

  products: Product[] = [];
  loading = true;
  editingProduct: Product | null = null;
  showCreateForm = false;
  formError: string | null = null;

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.loading = true;
    this.productService.getAll().subscribe({
      next: products => {
        this.products = products;
        this.loading = false;
      },
      error: () => { this.loading = false; }
    });
  }

  openCreate(): void {
    this.editingProduct = null;
    this.formError = null;
    this.showCreateForm = true;
  }

  openEdit(product: Product): void {
    this.editingProduct = product;
    this.formError = null;
    this.showCreateForm = false;
  }

  closeModal(): void {
    this.showCreateForm = false;
    this.editingProduct = null;
    this.formError = null;
  }

  create(payload: ProductPayload): void {
    this.productService.create(payload).subscribe({
      next: () => { this.closeModal(); this.loadProducts(); },
      error: err => { this.formError = err.error?.message ?? 'Failed to create product.'; }
    });
  }

  update(payload: ProductPayload): void {
    if (!this.editingProduct) return;
    this.productService.update(this.editingProduct.id, payload).subscribe({
      next: () => { this.closeModal(); this.loadProducts(); },
      error: err => { this.formError = err.error?.message ?? 'Failed to update product.'; }
    });
  }

  delete(product: Product): void {
    if (!confirm(`Delete "${product.name}"?`)) return;
    this.productService.remove(product.id).subscribe({
      next: () => this.loadProducts()
    });
  }

  get modalOpen(): boolean {
    return this.showCreateForm || this.editingProduct !== null;
  }

  get modalTitle(): string {
    return this.editingProduct ? 'Edit Product' : 'New Product';
  }
}
