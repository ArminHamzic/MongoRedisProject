import { Component, OnInit } from '@angular/core';
import { HttpClientService } from '../shared/http-client.service';
import { UtilsService } from '../shared/utils.service';
import { Post } from '../shared/model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  public posts: Post[];

  constructor(private readonly http: HttpClientService,
    private readonly utils: UtilsService) { }

  public ngOnInit(): void {
    this.loadPosts();
  }

  private loadPosts(): void {
    this.http.get<Post[]>('post', 'all', null)
      .subscribe(posts => {
        for (let p of posts) {
          // I admit this is pretty ugly, a display class performing the operation in the constructor would be better
          p.publishedStr = this.utils.formatRawDate(p.published, true);
        }
        this.posts = posts;
      },
        _ => {
          alert('failed to load posts');
        });
  }
}
