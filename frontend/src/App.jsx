import IncidentList from "./components/IncidentList";
import CreateIncident from "./components/CreateIncident";
import { useRef } from "react";

function App() {
  const listRef = useRef();

  return (
    <div style={{ padding: "20px" }}>
      <h2>Incident Management System</h2>

      <CreateIncident onCreated={() => window.location.reload()} />

      <IncidentList />
    </div>
  );
}

export default App;
