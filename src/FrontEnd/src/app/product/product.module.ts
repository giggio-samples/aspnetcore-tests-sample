import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductCreateComponent } from './product-create.component';
import { ProductRoutingModule } from './product-routing.module';
import { ProductComponent } from './product.component';

@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    ProductRoutingModule,
    CommonModule
  ],
  declarations: [ProductComponent, ProductCreateComponent],
  providers: []
})
export class ProductModule {}
