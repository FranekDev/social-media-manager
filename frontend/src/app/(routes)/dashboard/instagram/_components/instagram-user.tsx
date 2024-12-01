import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Link2, UserRound } from "lucide-react";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { InstagramUserDetail } from "@/types/instagram/response/instagram-user-detail";
import { Skeleton } from "@/components/ui/skeleton";
import UserDetail from "@/components/platforms/user-detail";

export default function InstagramProfile({ user, isLoading }: { user: InstagramUserDetail | null, isLoading: boolean }) {

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
                    <UserDetail value={user?.mediaCount}
                                label="Posty"
                                isLoading={isLoading} />

                    <UserDetail value={user?.followersCount}
                                label="Obserwujący"
                                isLoading={isLoading} />

                    <UserDetail value={user?.followsCount}
                                label="Obserwowani"
                                isLoading={isLoading} />
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
