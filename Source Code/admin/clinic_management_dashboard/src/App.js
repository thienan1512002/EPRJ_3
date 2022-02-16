import "./App.css";
import Login from "./Dashboard/login";
import User from "./Dashboard/user";
import Dashboards from "./Dashboard/dashboard";
import Medicine from "./Dashboard/medicine";
import Course from "./Dashboard/course";
import Order from "./Dashboard/order";
import EquipmentForClinic from "./Dashboard/equipment_for_clinic";
import EquipmentForEcomerce from "./Dashboard/equipment_for_ecomerce";
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
        <Route path="/user">
          <User />
        </Route>
        <Route path="/equipment-clinic">
          <EquipmentForClinic />
        </Route>
        <Route path="/equipment-ecomerce">
          <EquipmentForEcomerce />
        </Route>
        <Route path="/medicine">
          <Medicine />
        </Route>
        <Route path="/course">
          <Course />
        </Route>
        <Route path="/order">
          <Order />
        </Route>
      </Switch>
    </Router>
  );
}

export default App;
