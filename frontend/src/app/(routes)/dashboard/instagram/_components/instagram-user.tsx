import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Link2, UserRound } from "lucide-react";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { InstagramUserDetail } from "@/types/instagram/response/instagram-user-detail";
import { Skeleton } from "@/components/ui/skeleton";

export default function InstagramProfile({ user, isLoading }: { user: InstagramUserDetail | null, isLoading: boolean }) {
    const formatNumber = (num: number) => {
        if (num >= 1000000) {
            return `${(num / 1000000).toFixed(1)}M`;
        }
        if (num >= 1000) {
            return `${(num / 1000).toFixed(1)}K`;
        }
        return num.toString();
    };

    return (
        <Card className="w-full max-w-2xl">
            <CardHeader className="space-y-4">
                <div className="flex items-center gap-4">
                    {isLoading ? (
                        <Avatar className="w-24 h-24">
                            <Skeleton className="rounded w-full h-full"/>
                        </Avatar>
                    ) : (
                        <Avatar className="w-24 h-24">
                            <AvatarImage src={user?.profilePictureUrl}
                                         alt={user?.userName}/>
                            <AvatarFallback>
                                <UserRound className="w-12 h-12"/>
                            </AvatarFallback>
                        </Avatar>
                    )}

                    <div className="space-y-1">
                        <CardTitle className="text-2xl">{user?.userName}</CardTitle>
                        <div className="text-sm text-muted-foreground">{user?.name}</div>
                    </div>
                </div>

                <div className="grid grid-cols-3 gap-4 text-center">
                    <div className="flex flex-col">
                        {isLoading ? (
                            <Skeleton className="w-full h-4"/>
                        ) : (
                            <span className="font-bold">{user ? formatNumber(user.mediaCount) : ""}</span>
                        )}
                        <span className="text-sm text-muted-foreground">Posty</span>
                    </div>

                    <div className="flex flex-col">
                        {isLoading ? (
                            <Skeleton className="w-full h-4"/>
                        ) : (
                            <span className="font-bold">{user ? formatNumber(user.followersCount) : ""}</span>
                        )}
                        <span className="text-sm text-muted-foreground">Obserwujący</span>
                    </div>

                    <div className="flex flex-col">

                        {isLoading ? (
                            <Skeleton className="w-full h-4"/>
                        ) : (
                            <span className="font-bold">{user ? formatNumber(user.followsCount) : ""}</span>
                        )}
                        <span className="text-sm text-muted-foreground">Obserwowani</span>
                    </div>
                </div>
            </CardHeader>

            <CardContent className="space-y-4">
                <div className="space-y-2">
                    {isLoading ? (
                        <Skeleton className="w-full h-4"/>
                    ) : (
                        <>
                            {user?.biography && (
                                <p className="text-sm whitespace-pre-wrap">{user?.biography}</p>
                            )}

                            {user?.website && (
                                <a
                                    href={user?.website}
                                    target="_blank"
                                    rel="noopener noreferrer"
                                    className="flex items-center gap-2 text-sm text-blue-600 hover:underline"
                                >
                                    <Link2 className="w-4 h-4"/>
                                    {user?.website}
                                </a>
                            )}
                        </>
                    )}
                </div>
            </CardContent>
        </Card>
    );
};
