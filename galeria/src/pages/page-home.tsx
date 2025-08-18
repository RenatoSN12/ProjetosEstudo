import Container from "../components/container";
import PhotoWidget from "../contexts/photos/components/photo-widget";

export default function PageHome() {
  return (
    <>
      <Container>
        <div className="grid grid-cols-4 gap-4">
          <PhotoWidget
            photo={{
              id: "123",
              title: "Olá mundo",
              imageId: "portrait-tower.png",
              albums: [
                { id: "123", title: "Teste" },
                { id: "321", title: "Teste2" },
              ],
            }}
          />
          <PhotoWidget
            photo={{
              id: "123",
              title: "Olá mundo",
              imageId: "portrait-tower.png",
              albums: [
                { id: "123", title: "Teste" },
                { id: "321", title: "Teste2" },
              ],
            }}
          />
          <PhotoWidget
            photo={{
              id: "123",
              title: "Olá mundo",
              imageId: "portrait-tower.png",
              albums: [
                { id: "123", title: "Teste" },
                { id: "321", title: "Teste2" },
              ],
            }}
          />
          <PhotoWidget
            photo={{
              id: "123",
              title: "Olá mundo",
              imageId: "portrait-tower.png",
              albums: [
                { id: "123", title: "Teste" },
                { id: "321", title: "Teste2" },
              ],
            }}
          />
        </div>
      </Container>
    </>
  );
}
