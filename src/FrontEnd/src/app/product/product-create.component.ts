import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Product } from './product';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.scss']
})
export class ProductCreateComponent implements OnInit {
  public name = new FormControl('');
  public price = new FormControl('');
  protected url = 'http://localhost:5000/api/products';
  constructor(protected httpClient: HttpClient, private router: Router) {}

  ngOnInit() {}

  create() {
    const product: Product = {
      name: this.name.value,
      price: this.price.value
    };
    this.httpClient
      .post<Product>(`${this.url}`, product)
      .subscribe(
        () => this.router.navigate(['/product']),
        error => alert('Error adding product: ' + JSON.stringify(error))
      );
  }
}
