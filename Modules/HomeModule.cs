using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Appointment> allAppointments = Appointment.GetAll();
        return View["index.cshtml", allAppointments];
      };

      Get["/stylist/new"] = _ => {
        return View ["stylist_form.cshtml"];
      };
      Post["/stylist/new"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["stylist_name"]);
        newStylist.Save();
        return View["success.cshtml", newStylist];
      };

      Get["/stylist/all"] = _ => {
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };

      Get["stylist/{id}"] = parameters => {
        Dictionary<string, object> Model = new Dictionary<string, object>();
        var foundStylist = Stylist.Find(parameters.id);
        var foundClients = foundStylist.GetClients();
        var foundApppointments = foundStylist.GetAppointments();
        Model.Add("stylist", foundStylist);
        Model.Add("clients", foundClients);
        Model.Add("appointments", foundApppointments);
        return View["stylist.cshtml", Model];
      };

      Get["stylist/update/{id}"] = parameters => {
        var foundStylist = Stylist.Find(parameters.id);
        return View["stylist_update.cshtml", foundStylist];
      };
      Patch["stylist/update/{id}"] = parameters => {
        Stylist selectedStylist = Stylist.Find(parameters.id);
        selectedStylist.Update(Request.Form["stylist_name"]);
        return View["success.cshtml", selectedStylist];
      };

      Get["stylist/delete/{id}"] = parameters => {
        Stylist selectedStylist = Stylist.Find(parameters.id);
        return View["stylist_delete.cshtml", selectedStylist];
      };
      Delete["stylist/delete/{id}"] = parameters => {
        Stylist selectedStylist = Stylist.Find(parameters.id);
        string stylistName = selectedStylist.GetName();
        selectedStylist.Delete();
        return View["deleted.cshtml", stylistName];
      };

      Get["/client/new"] = _ => {
        List<Stylist> allStylists = Stylist.GetAll();
        return View ["client_form.cshtml", allStylists];
      };
      Post["/client/new"] = _ => {
        Client newClient = new Client(Request.Form["client_name"], Request.Form["stylist_id"]);
        newClient.Save();
        return View["success.cshtml", newClient];
      };
      Get["/client/all"] = _ => {
        List<Client> allClients = Client.GetAll();
        return View["clients.cshtml", allClients];
      };
      Get["client/{id}"] = parameters => {
        Dictionary<string, object> Model = new Dictionary<string, object>();
        var foundClient = Client.Find(parameters.id);
        var foundStylist = Stylist.Find(foundClient.GetStylistId());

        var foundAppointments = foundClient.GetAppointments();
        Model.Add("stylist", foundStylist);
        Model.Add("client", foundClient);
        Model.Add("appointments", foundAppointments);
        return View["client.cshtml", Model];
      };

      Get["client/delete/{id}"] = parameters => {
        Client selectedClient = Client.Find(parameters.id);
        return View["client_delete.cshtml", selectedClient];
      };
      Delete["client/delete/{id}"] = parameters => {
        Client selectedClient = Client.Find(parameters.id);
        string clientName = selectedClient.GetName();
        selectedClient.Delete();
        return View["deleted.cshtml", clientName];
      };

      Get["client/update/{id}"] = parameters => {
        Dictionary<string, object> Model = new Dictionary<string, object>();
        var foundClient = Client.Find(parameters.id);
        List<Stylist> allStylists = Stylist.GetAll();
        Model.Add("client", foundClient);
        Model.Add("stylists", allStylists);
        return View["client_update.cshtml", Model];
      };
      Patch["client/update/{id}"] = parameters => {
        Client selectedClient = Client.Find(parameters.id);
        selectedClient.Update(Request.Form["client_name"], Request.Form["stylist_id"]);
        return View["success.cshtml", selectedClient];
      };

      Get["/appointment/new"] = _ => {
        List<Client> allClients = Client.GetAll();
        return View ["appointment_form.cshtml", allClients];
      };
      Post["/appointment/new"] = _ => {
        var appointmentClient = Client.Find(Request.Form["client_id"]);
        var stylistId = appointmentClient.GetStylistId();
        Appointment newAppointment = new Appointment(Request.Form["client_id"], stylistId, Request.Form["appointment_date"], Request.Form["appointment_duration"]);
        newAppointment.Save();
        return View["scheduled.cshtml", newAppointment];
      };




    }
  }
}
