"use client";

import { useEffect, useState } from "react";
import { getFacebookPageData } from "@/features/facebook/api/get-facebook-page-data";
import { useAuth } from "@/hooks/use-auth";
import { FacebookPageData } from "@/types/facebook/facebook-page-data";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Skeleton } from "@/components/ui/skeleton";
import { UserRound } from "lucide-react";
import UserDetail from "@/components/platforms/user-detail";

type FacebookPageProps = {
    pageId: string;
    coverUrl: string;
};

export default function FacebookPage({ pageId, coverUrl }: FacebookPageProps) {

    const { token } = useAuth();
    const [fbPageData, setFbPageData] = useState<FacebookPageData | null>(null);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const fetchFbPageData = async () => {
            const { data, errors } = await getFacebookPageData(token, pageId);
            setFbPageData(data);
            setIsLoading(false);
        };

        fetchFbPageData();
    }, [token, pageId]);
    return (
        <Card className="w-fit max-w-2xl">
            <CardHeader>
                <div className="flex w-full gap-8">
                    <div className="flex items-center gap-4 w-fit">
                        {isLoading ? (
                            <Avatar className="w-24 h-24">
                                <Skeleton className="rounded w-full h-full"/>
                            </Avatar>
                        ) : (
                            <Avatar className="w-24 h-24">
                                <AvatarImage
                                    src={coverUrl}
                                    alt={fbPageData?.name}
                                />
                                <AvatarFallback>
                                    <UserRound className="w-12 h-12"/>
                                </AvatarFallback>
                            </Avatar>
                        )}

                        <div className="space-y-1">
                            <CardTitle className="text-2xl">{fbPageData?.name}</CardTitle>
                            <div className="text-sm text-muted-foreground">{fbPageData?.category}</div>
                            <p>{fbPageData?.about}</p>
                        </div>
                    </div>

                    <div className="text-center w-fit flex justify-center items-center">
                        <UserDetail
                            value={fbPageData?.followersCount}
                            label="Obserwujący"
                            isLoading={isLoading}
                        />
                    </div>
                </div>
            </CardHeader>

            {fbPageData?.categoryList && fbPageData.categoryList.length > 0 &&
                <CardContent>
                    <div className="space-y-2">
                        {isLoading ? (
                            <Skeleton className="w-full h-4"/>
                        ) : (
                            <div className="text-sm">
                                <strong>Kategorie:</strong>
                                <ul className="list-disc list-inside">
                                    {fbPageData.categoryList.map((category) => (
                                        <li key={category.id}>{category.name}</li>
                                    ))}
                                </ul>
                            </div>
                        )}
                    </div>
                </CardContent>
            }
        </Card>
    );
}