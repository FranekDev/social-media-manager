export interface From {
    name: string;
    id: string;
}

export interface FacebookPagePostComment {
    createdTime: string;
    from: From;
    message: string;
    id: string;
}