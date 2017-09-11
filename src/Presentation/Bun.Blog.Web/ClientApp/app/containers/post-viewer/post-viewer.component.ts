import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Location } from '@angular/common';

import 'rxjs/add/operator/switchMap';

import { IPostDetail } from '../../models/Posts/IPostDetail'
import { PostService } from '../../shared/post.service'


@Component({
    selector: "post-viewer",
    templateUrl: "./post-viewer.component.html"
})

export class PostViewer implements OnInit {
    constructor(
        private route: ActivatedRoute,
        private location: Location,
        private postService: PostService
    ) { }

    postDetail: IPostDetail;

    ngOnInit(): void {
        this.route.paramMap
            .switchMap((params: ParamMap) => this.postService.getPostById(+params.get("id")))
            .subscribe(postDetail => {
                this.postDetail = postDetail;
                console.log(postDetail);
            });
    }
}
