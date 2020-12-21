import { Injectable } from '@angular/core';
import { UtilsService } from './utils.service';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HttpClientService {
  private readonly baseUrl: string;

  constructor(private readonly utils: UtilsService,
    private readonly http: HttpClient) {
    this.baseUrl = 'http://localhost:5000/';
  }

  private createUrl(controller: string, action: string): string {
    let url = `${this.baseUrl}api/${controller}`;
    if (!this.utils.isNullOrWhiteSpace(action)) {
      url += `/${action}`;
    }
    return url;
  }

  public get<T>(controller: string, action: string, params: [string, string][]): Observable<T> {
    var gdp = this.prepareGetDelete(controller, action, params);
    if (gdp.params !== null) {
      return this.http.get<T>(gdp.url, { params: gdp.params });
    }
    return this.http.get<T>(gdp.url);
  }

  public delete(controller: string, action: string, params: [string, string][]): Observable<Object> {
    var gdp = this.prepareGetDelete(controller, action, params);
    if (gdp.params !== null) {
      return this.http.delete(gdp.url, { params: gdp.params })
    }
    return this.http.delete(gdp.url);
  }

  public postJson<T>(controller: string, action: string, data: any): Observable<T> {
    this.validatePostPutInput(controller, data);
    const ppp = this.preparePostPut(controller, action, data);

    return this.http.post<T>(ppp.url, ppp.payload, {
      headers: ppp.headers
    });
  }

  public putJson<T>(controller: string, action: string, data: any): Observable<T> {
    this.validatePostPutInput(controller, data);
    const ppp = this.preparePostPut(controller, action, data);

    return this.http.put<T>(ppp.url, ppp.payload, {
      headers: ppp.headers
    });
  }

  private prepareGetDelete(controller: string, action: string, params: [string, string][]) {
    const url = this.createUrl(controller, action);
    let p: HttpParams | null = null;
    if (!this.utils.isNullOrUndefined(params) && params.length > 0) {
      p = new HttpParams();
      for (let t of params) {
        p = p.append(t[0], t[1]);
      }
    }
    return new GetDeleteParts(url, p);
  }

  private preparePostPut(controller: string, action: string, data: any): PostPutParts {
    const payload = JSON.stringify(data);
    const url = this.createUrl(controller, action);
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    return new PostPutParts(payload, url, headers);
  }

  private validatePostPutInput(controller: string, data: any): void {
    if (this.utils.isNullOrUndefined(this.http) ||
      this.utils.isNullOrUndefined(data) ||
      this.utils.isNullOrWhiteSpace(controller)) {
      throw new Error('argument null');
    }
  }
}

class PostPutParts {
  constructor(public readonly payload: string, public readonly url: string, public readonly headers: HttpHeaders) { }
}

class GetDeleteParts {
  constructor(public readonly url: string, public readonly params: HttpParams) { }
}
