export interface Post {
    id: string;
    title: string;
    text: string;
    author: string;
    published: Date;
    publishedStr: string;
}

export interface Comment {
    name: string,
    text: string,
    created: Date,
    createdStr: string;
}