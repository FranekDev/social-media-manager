export enum InstagramMediaProductType {
    Feed = "FEED",
    Story = "STORY",
    Reel = "REEL",
    IGTV = "IGTV",
    Shopping = "SHOPPING"
}

export enum InstagramMediaType {
    Image = 0,
    Video = 1,
    CarouselAlbum = 2
}

export function getMediaTypeString(mediaType: InstagramMediaType): string {
    switch (mediaType) {
        case InstagramMediaType.Image:
            return "Image";
        case InstagramMediaType.Video:
            return "Video";
        case InstagramMediaType.CarouselAlbum:
            return "Carousel Album";
        default:
            return "Unknown";
    }
}