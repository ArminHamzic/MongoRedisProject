import { Component, OnInit, Input } from '@angular/core';
import { HttpClientService } from '../shared/http-client.service';
import { UtilsService } from '../shared/utils.service';
import { Comment } from '../shared/model';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html'
})
export class CommentsComponent implements OnInit {

  @Input()
  public postId: string;
  public comments: Comment[];
  public newCommentName: string;
  public newCommentText: string;
  public newCommentMail: string;

  constructor(private readonly http: HttpClientService,
    private readonly utils: UtilsService) {
    this.comments = null;
  }

  public ngOnInit(): void {
    this.loadComments();
  }

  public createComment(): void {
    if (this.utils.isNullOrWhiteSpace(this.newCommentName)
      || this.utils.isNullOrWhiteSpace(this.newCommentMail)
      || this.utils.isNullOrWhiteSpace(this.newCommentText)) {
      alert('all fields have to be filled');
      return;
    }
    this.http.postJson<Comment>('comment', null, {
      name: this.newCommentName,
      mail: this.newCommentMail,
      text: this.newCommentText,
      postId: this.postId
    })
      .subscribe(comment => {
        comment.createdStr = this.utils.formatRawDate(comment.created, true);
        if (this.comments === null){
          this.comments = [comment];
          return;
        }
        this.comments.push(comment);
      },
        _ => {
          alert('failed to create comment');
        });
  }

  private loadComments(): void {
    this.http.get<Comment[]>('comment', 'post', [['postId', this.postId]])
      .subscribe(comments => {
        if (comments.length === 0) {
          return;
        }
        for (let c of comments) {
          c.createdStr = this.utils.formatRawDate(c.created, true);
        }
        this.comments = comments;
      },
        _ => {
          alert('failed to load post');
        });
  }
}
