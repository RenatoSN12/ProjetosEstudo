import Container from "../components/container";
import TaskItem from "../core-components/task-item";
import TasksSummary from "../core-components/tasks-summary";

export default function HomePage() {
  return (
    <Container as="article" className="space-y-3">
      <header className="flex items-center justify-between">
        <TasksSummary />
      </header>
      <TaskItem />
    </Container>
  );
}
