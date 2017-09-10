import { Component, OnInit } from '@angular/core';

import { PostService } from '../../shared/post.service'

@Component({
    selector: "test-post-list",
    templateUrl: "./test-post-list.component.html"
})

export class TestPostListComponent implements OnInit {

    message: string;

    constructor(private postService: PostService) {

    }

    ngOnInit(): void {
        this.message= this.postService.getPosts();
        
    }

}
