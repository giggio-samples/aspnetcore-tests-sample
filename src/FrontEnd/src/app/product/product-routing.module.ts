import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductCreateComponent } from './product-create.component';
import { ProductComponent } from './product.component';

const routes: Routes = [
  { path: 'product/create', component: ProductCreateComponent },
  { path: 'product', component: ProductComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductRoutingModule {}
