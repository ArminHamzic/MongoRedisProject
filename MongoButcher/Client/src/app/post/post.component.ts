import { Component, OnInit } from '@angular/core';
import { HttpClientService } from '../shared/http-client.service';
import { UtilsService } from '../shared/utils.service';
import { ActivatedRoute } from '@angular/router'
import { Post } from '../shared/model';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html'
})
export class PostComponent implements OnInit {

  private id: string;
  public post: Post;

  constructor(private readonly http: HttpClientService,
    private readonly utils: UtilsService,
    private readonly route: ActivatedRoute) {
    this.post = null;
  }

  public ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.id = params.get('id');
    });
    this.loadPost();
  }

  public deletePost(): void {
    this.http.delete('post', null, [["id", this.id]])
      .subscribe(_ => {
        alert(`deleted post ${this.id}`);
      }, __ => {
        alert(`failed to delete post ${this.id}`);
      })
  }

  private loadPost(): void {
    this.http.get<Post>('post', null, [['id', this.id]])
      .subscribe(p => {
        p.publishedStr = this.utils.formatRawDate(p.published, true);
        this.post = p;
      },
        _ => {
          alert(`failed to load post ${_}`);
        });
  }
}
