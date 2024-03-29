using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalon
{
  public class Appointment
  {
    private int _id;
    private int _clientId;
    private int _stylistId;
    private DateTime _dateAndTime;
    private int _durationMinutes;

    public Appointment (int clientId, int stylistId, DateTime dateAndTime, int durationMinutes, int id =0)
    {
      _id = id;
      _clientId = clientId;
      _stylistId = stylistId;
      _dateAndTime = dateAndTime;
      _durationMinutes = durationMinutes;
    }

    public int GetId()
    {
      return _id;
    }
    public int GetClientId()
    {
      return _clientId;
    }
    public int GetStylistId()
    {
      return _stylistId;
    }
    public DateTime GetDate()
    {
      return _dateAndTime;
    }
    public int GetDuration()
    {
      return _durationMinutes;
    }

    public void SetClientId(int clientId)
    {
      _clientId = clientId;
    }
    public void SetStylistId(int stylistId)
    {
      _stylistId = stylistId;
    }
    public void SetDate(DateTime date)
    {
      _dateAndTime = date;
    }
    public void SetDuration(int duration)
    {
      _durationMinutes = duration;
    }

    public override bool Equals(System.Object otherAppointment)
    {
      if (!(otherAppointment is Appointment))
      {
        return false;
      }
      else
      {
        Appointment newAppointment = (Appointment) otherAppointment;
        bool idEquality = this.GetId() == newAppointment.GetId();
        bool clientIdEquality = this.GetClientId() == newAppointment.GetClientId();
        bool stylistIdEquality = this.GetStylistId() == newAppointment.GetStylistId();
        bool dateEquality = this.GetDate() == newAppointment.GetDate();
        bool durationEquality = this.GetDuration() == newAppointment.GetDuration();
        return (idEquality && clientIdEquality && stylistIdEquality && dateEquality && durationEquality);
      }
    }

    public static List<Appointment> GetAll()
    {
      List<Appointment> allAppointments = new List<Appointment> {};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM appointments;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int appointmentId = rdr.GetInt32(0);
        int clientId = rdr.GetInt32(1);
        int stylistId = rdr.GetInt32(2);
        DateTime dateAndTime = rdr.GetDateTime(3);
        int duration = rdr.GetInt32(4);

        Appointment newAppointment = new Appointment(clientId, stylistId, dateAndTime, duration, appointmentId);
        allAppointments.Add(newAppointment);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allAppointments;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO appointments (client_id, stylist_id, date_and_time, duration) OUTPUT INSERTED.id VALUES (@ClientId, @StylistId, @DateAndTime, @Duration);", conn);

      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = this.GetClientId();
      cmd.Parameters.Add(clientIdParameter);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetStylistId();
      cmd.Parameters.Add(stylistIdParameter);

      SqlParameter dateAndTimeParameter = new SqlParameter();
      dateAndTimeParameter.ParameterName = "@DateAndTime";
      dateAndTimeParameter.Value = this.GetDate();
      cmd.Parameters.Add(dateAndTimeParameter);

      SqlParameter durationParameter = new SqlParameter();
      durationParameter.ParameterName = "@Duration";
      durationParameter.Value = this.GetDuration();
      cmd.Parameters.Add(durationParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Appointment Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM appointments WHERE id = @searchId;", conn);

      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@searchId";
      clientIdParameter.Value = id.ToString();

      cmd.Parameters.Add(clientIdParameter);
      rdr = cmd.ExecuteReader();

      int foundAppointmentId = 0;
      int foundClientId = 0;
      int foundStylistId = 0;
      DateTime foundDate = new DateTime();
      int foundDuration =0;

      while (rdr.Read())
      {
        foundAppointmentId = rdr.GetInt32(0);
        foundClientId = rdr.GetInt32(1);
        foundStylistId = rdr.GetInt32(2);
        foundDate = rdr.GetDateTime(3);
        foundDuration = rdr.GetInt32(4);
      }
      Appointment foundAppointment = new Appointment(foundClientId, foundStylistId, foundDate, foundDuration, foundAppointmentId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundAppointment;
    }

    public void Update(int newClientId, int newStylistId, DateTime newDate, int newDuration)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE appointments SET client_id = @NewClientId, stylist_id = @NewStylistId, date_and_time = @NewDate, duration = @NewDuration OUTPUT INSERTED.client_id, INSERTED.stylist_id, INSERTED.date_and_time, INSERTED.duration where id = @AppointmentId;", conn);

      SqlParameter newClientIdParameter = new SqlParameter();
      newClientIdParameter.ParameterName = "@NewClientId";
      newClientIdParameter.Value = newClientId;
      cmd.Parameters.Add(newClientIdParameter);

      SqlParameter newStylistIdParameter = new SqlParameter();
      newStylistIdParameter.ParameterName = "@NewStylistId";
      newStylistIdParameter.Value = newStylistId;
      cmd.Parameters.Add(newStylistIdParameter);

      SqlParameter newDateParameter = new SqlParameter();
      newDateParameter.ParameterName = "@NewDate";
      newDateParameter.Value = newDate;
      cmd.Parameters.Add(newDateParameter);

      SqlParameter newDurationParameter = new SqlParameter();
      newDurationParameter.ParameterName = "@NewDuration";
      newDurationParameter.Value = newDuration;
      cmd.Parameters.Add(newDurationParameter);

      SqlParameter appointmentIdParameter = new SqlParameter();
      appointmentIdParameter.ParameterName = "@AppointmentId";
      appointmentIdParameter.Value = this.GetId();
      cmd.Parameters.Add(appointmentIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._clientId = rdr.GetInt32(0);
        this._stylistId = rdr.GetInt32(1);
        this._dateAndTime = rdr.GetDateTime(2);
        this._durationMinutes = rdr.GetInt32(3);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM appointments WHERE id = @AppointmentId;", conn);

      SqlParameter appointmentIdParameter = new SqlParameter();
      appointmentIdParameter.ParameterName = "@AppointmentId";
      appointmentIdParameter.Value = this.GetId();

      cmd.Parameters.Add(appointmentIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM appointments;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
