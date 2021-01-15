import { Injectable, } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {Product} from '../../data/product';
import {ResourceService} from '../../core/http/resource.service';


@Injectable({
  providedIn: 'root'
})
export class ProductResolver implements Resolve<Product> {
  constructor(
    private productService: ResourceService,
    private router: Router
  ) { }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    return this.productService.getResourcesById(route.params.id)
      .pipe(catchError((err) => this.router.navigateByUrl('/')));
  }
}
