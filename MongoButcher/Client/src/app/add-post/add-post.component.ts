import { Component } from '@angular/core';
import { HttpClientService } from '../shared/http-client.service';
import { UtilsService } from '../shared/utils.service';
import { Post } from '../shared/model';

@Component({
  selector: 'app-add-post',
  templateUrl: './add-post.component.html'
})
export class AddPostComponent {

  public title: string;
  public text: string;
  public author: string;

  constructor(private readonly http: HttpClientService,
    private readonly utils: UtilsService) { }

  public createPost(): void {
    if (this.utils.isNullOrWhiteSpace(this.title)
      || this.utils.isNullOrWhiteSpace(this.text)
      || this.utils.isNullOrWhiteSpace(this.author)) {
      alert('all fields need to be filled');
      return;
    }
    this.http.postJson<Post>('post', null, {
      title: this.title,
      author: this.author,
      text: this.text
    })
      .subscribe(post => {
        alert(`new id: ${post.id.toString()}`);
      },
        _ => {
          alert('failed to create post');
        });
  }

}
