import Text from "./components/text";
import TrashIcon from "./assets/icons/trash.svg?react";
import Icon from "./components/icon";
import SpinnerIcon from "./assets/icons/spinner.svg?react";
import Badge from "./components/badge";
import Button from "./components/button";
import PlusIcon from "./assets/icons/plus.svg?react";

export default function App() {
  return (
    <div className="grid gap-3">
      <div className="flex flex-col gap-1">
        <Text variant="body-sm-bold" className="text-pink-base">
          Olá mundo
        </Text>
      </div>
      <div className="flex gap-1">
        <Icon svg={TrashIcon} />
        <Icon svg={SpinnerIcon} animate />
      </div>
      <div>
        <Badge variant={"secondary"}>5</Badge>
        <Badge>2 de 5</Badge>
      </div>
      <div>
        <Button icon={PlusIcon}>Novo!</Button>
      </div>
    </div>
  );
}
