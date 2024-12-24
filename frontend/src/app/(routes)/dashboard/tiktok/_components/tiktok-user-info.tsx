import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Link2, UserRound } from "lucide-react";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Skeleton } from "@/components/ui/skeleton";
import UserDetail from "@/components/platforms/user-detail";
import { TikTokUser } from "@/types/tiktok/response/tiktok-user";

export default function TikTokUserProfile({ user, isLoading }: { user: TikTokUser | null, isLoading: boolean }) {

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
                            <AvatarImage src={user?.avatarUrl} alt={user?.username}/>
                            <AvatarFallback>
                                <UserRound className="w-12 h-12"/>
                            </AvatarFallback>
                        </Avatar>
                    )}

                    <div className="space-y-1">
                        <CardTitle className="text-2xl">{user?.displayName}</CardTitle>
                        <div className="text-sm text-muted-foreground">{user?.username}</div>
                    </div>
                </div>

                <div className="grid grid-cols-3 gap-4 text-center">
                    <UserDetail value={user?.videoCount} label="Filmy" isLoading={isLoading} />
                    <UserDetail value={user?.followerCount} label="Obserwujący" isLoading={isLoading} />
                    <UserDetail value={user?.followingCount} label="Obserwowani" isLoading={isLoading} />
                </div>
            </CardHeader>

            <CardContent className="space-y-4">
                <div className="space-y-2">
                    {isLoading ? (
                        <Skeleton className="w-full h-4"/>
                    ) : (
                        <>
                            {user?.bioDescription && (
                                <p className="text-sm whitespace-pre-wrap">{user?.bioDescription}</p>
                            )}
                        </>
                    )}
                </div>
            </CardContent>
        </Card>
    );
}