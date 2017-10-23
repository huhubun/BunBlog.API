import { Injectable, Inject } from '@angular/core';
import { Http, URLSearchParams } from '@angular/http';
import { TransferHttp } from '../../modules/transfer-http/transfer-http';
import { Observable } from 'rxjs/Observable';
import { ORIGIN_URL } from './constants/baseurl.constants';
import { IPostListItem } from '../models/Posts/IPostListItem'
import { IPostDetail } from '../models/Posts/IPostDetail'

@Injectable()
export class PostService {
    constructor(
        private transferHttp: TransferHttp,
        private http: Http,
        @Inject(ORIGIN_URL) private baseUrl: string) {
    }

    getPosts(): Observable<IPostListItem[]> {
      return this.transferHttp.get(`${this.baseUrl}/api/posts`);
    }

    getPostById(id: number): Observable<IPostDetail> {
      return this.transferHttp.get(`${this.baseUrl}/api/posts/${id}`);
    }

    getString(): string {
        return "called!!";
    }
}
