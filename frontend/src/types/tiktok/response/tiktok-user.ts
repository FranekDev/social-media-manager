export type TikTokUser = {
    avatarUrl: string;
    openId: string;
    unionId: string;
    displayName: string;
    bioDescription: string;
    username: string;
    followerCount: number;
    followingCount: number;
    likesCount: number;
    videoCount: number;
    isVerified: boolean;
};

export type TikTokUserResponse = {
    user: TikTokUser;
};
