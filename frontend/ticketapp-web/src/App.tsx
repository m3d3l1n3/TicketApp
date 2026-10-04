import {Routes, Route} from "react-router-dom";
import { TicketList } from "./components/TicketList";
import {TicketDetails} from "./components/TicketDetails";
import { TicketForm } from "./components/TicketForm";

function App() {
  return (
    <Routes>
      <Route path ="/" element ={<TicketList/>}/>
      <Route path="/tickets/:id" element = {<TicketDetails/>}/>
      <Route path="/tickets/new" element = {<TicketForm/>}/>

    </Routes>
  )
}

export default App;