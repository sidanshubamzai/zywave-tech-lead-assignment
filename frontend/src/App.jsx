import IncidentList from "./components/IncidentList";
import CreateIncident from "./components/CreateIncident";
import { useRef } from "react";

function App() {
  const listRef = useRef();

  return (
    <div
      style={{
        minHeight: "100vh",
        backgroundColor: "#f4f6f8",
        padding: "40px 0",
      }}
    >
      <div
        style={{
          maxWidth: "1000px",
          margin: "0 auto",
          backgroundColor: "#ffffff",
          padding: "24px",
          borderRadius: "6px",
          boxShadow: "0 2px 6px rgba(0,0,0,0.05)",
        }}
      >
        <h2 style={{ marginBottom: "20px", textAlign:"center" }}>
          Incident Management System
        </h2>

        <div
          style={{
            backgroundColor: "#fafafa",
            padding: "16px",
            borderRadius: "4px",
            marginBottom: "24px",
          }}
        >
          <CreateIncident onCreated={() => window.location.reload()} />
        </div>

        <IncidentList />
      </div>
    </div>
  );
}

export default App;
