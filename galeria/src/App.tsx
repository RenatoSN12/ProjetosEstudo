import Button from "./components/button";
import ButtonIcon from "./components/button-icon";
import ChevronLeftIcon from "./assets/icons/chevron-left.svg?react";
import ChevronRightIcon from "./assets/icons/chevron-right.svg?react";
import Badge from "./components/badge";
import Alert from "./components/alert";
import Divider from "./components/divider";
import { Dialog, DialogBody, DialogContent, DialogFooter, DialogHeader, DialogTrigger } from "./components/dialog";
import Text from "./components/text";
import { DialogClose } from "@radix-ui/react-dialog";

export default function App() {
	return (
		<div className="grid gap-7 p-6">
			<div className="flex gap-3">
				<Button>Button</Button>
				<Button variant="secondary">Button</Button>
				<Button disabled>Button</Button>
				<Button handling>Loading</Button>
				<Button icon={ChevronRightIcon}>Próxima Imagem</Button>
				<Button variant="ghost" size="sm">
					Button
				</Button>
				<Button variant="primary" size="sm">
					Button
				</Button>
			</div>

			<div className="flex gap-3">
				<ButtonIcon icon={ChevronLeftIcon} />
				<ButtonIcon icon={ChevronRightIcon} variant="secondary" />
			</div>

			<div className="flex gap-3">
				<Badge>Todos</Badge>
				<Badge>Natureza</Badge>
				<Badge>Viagem</Badge>
				<Badge loading>Viagem</Badge>
				<Badge loading>Viagem</Badge>
				<Badge loading>Viagem</Badge>
			</div>

			<div>
				<Alert>
					Tamanho máximo: 50MB
					<br />
					Você pode selecionar arquivos em PNG, JPG, JPEG ou WEBP
				</Alert>
			</div>

			<div>
			  <Dialog>
				<DialogTrigger asChild>
				  <Button>Abrir Modal</Button>
				</DialogTrigger>
				<DialogContent>
				  <DialogHeader>Teste Dialog</DialogHeader>
				  <DialogBody>
					<Text>
					  Teste Conteúdo do Dialog
					</Text>
				  </DialogBody>
				  <DialogFooter>
					<DialogClose asChild>
					  <Button variant="secondary">Cancelar</Button>
					</DialogClose>
					<Button>Adicionar</Button>
				  </DialogFooter>
				</DialogContent>
			  </Dialog>
			</div>

			<div>
				<Divider />
			</div>
		</div>
	);
}
