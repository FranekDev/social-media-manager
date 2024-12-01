import { Skeleton } from "@/components/ui/skeleton";
import { formatNumber } from "@/lib/utils";

type UserDetailProps = {
    value: number | null | undefined;
    label: string;
    isLoading: boolean;
};

export default function UserDetail({ value, label, isLoading }: UserDetailProps) {
    return (
        <div className="flex flex-col">
            {isLoading ? (
                <Skeleton className="w-full h-4"/>
            ) : (
                <span className="font-bold">{formatNumber(value ?? 0)}</span>
            )}
            <span className="text-sm text-muted-foreground">{label}</span>
        </div>
    );
}