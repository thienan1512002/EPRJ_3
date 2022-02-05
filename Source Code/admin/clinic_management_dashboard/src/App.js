import "./App.css";
import Login from "./Dashboard/login";
import Dashboards from "./Dashboard/dashboard";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
function App() {
  return (
    <Router>
      <Switch>
        <Route exact path="/">
          <Login />
        </Route>
        <Route path="/admin">
          <Dashboards />
        </Route>
      </Switch>
    </Router>
  );
}

export default App;
