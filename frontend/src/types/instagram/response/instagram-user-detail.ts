export type InstagramUserDetail = {
    userName: string;
    biography: string;
    mediaCount: number;
    followersCount: number;
    followsCount: number;
    name: string;
    profilePictureUrl: string;
    website: string;
    media: InstagramMediaData;
};

export type InstagramMediaData = {
    data: InstagramMedia[];
};

export type InstagramMedia = {
    id: string;
};
