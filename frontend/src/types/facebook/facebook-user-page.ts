export interface Category {
    id: string;
    name: string;
}

export interface FacebookPagePicture {
    height: number;
    isSilhouette: boolean;
    url: string;
    width: number;
}

export interface FacebookUserPage {
    category: string;
    categoryList: Category[];
    name: string;
    pageId: string;
    tasks: string[];
    pagePicture?: FacebookPagePicture;
}