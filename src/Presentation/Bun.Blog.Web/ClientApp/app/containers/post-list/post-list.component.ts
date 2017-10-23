import { Component, OnInit } from '@angular/core';

import { IPostListItem } from '../../models/Posts/IPostListItem'
import { PostService } from '../../shared/post.service'

@Component({
    selector: "post-list",
    templateUrl: "./post-list.component.html",
    styleUrls: ["./post-list.component.css"]
})

export class TestPostListComponent implements OnInit {

    message: string;
    posts: IPostListItem[];

    constructor(private postService: PostService) {

    }

    ngOnInit(): void {
        this.message = this.postService.getString();
        this.postService.getPosts().subscribe(result => {
            console.log(result);
            this.posts = result;
        });
    }

}
