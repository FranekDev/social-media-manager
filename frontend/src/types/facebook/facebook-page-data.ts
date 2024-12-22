import { Category } from "@/types/facebook/facebook-user-page";

export interface ConnectedInstagramAccount {
    id: string;
}

export interface FacebookPageData {
    id: string;
    about: string;
    category: string;
    categoryList: Category[];
    checkins: number;
    connectedInstagramAccount: ConnectedInstagramAccount;
    followersCount: number;
    isPublished: boolean;
    link: string;
    name: string;
    ratingCount: number;
}