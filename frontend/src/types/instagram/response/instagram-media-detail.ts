import { InstagramMediaProductType, InstagramMediaType } from "../enums/instagram-media-enums";

export type InstagramMediaDetail = {
    caption: string;
    commentsCount: number;
    id: string;
    isCommentEnabled: boolean;
    isSharedToFeed: boolean;
    likeCount: number;
    mediaProductType: InstagramMediaProductType;
    mediaType: InstagramMediaType;
    mediaUrl: string;
    owner: InstagramMediaOwner;
    permalink: string;
    shortcode: string;
    thumbnailUrl: string;
    timestamp: string;
    username: string;
};

export type InstagramMediaOwner = {
    id: string;
};