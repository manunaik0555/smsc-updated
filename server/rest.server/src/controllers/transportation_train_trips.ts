
import { Response, Request } from "express";
import getDatabaseConnection from "../databases/mysql";

export async function getAll(request: Request, response: Response) {

  const sql = 'SELECT * FROM transportation_train_trips';
  const connection = await getDatabaseConnection();

  connection.query(sql, (err, rows) => {
    if (err) {
      console.error('Error querying the database: ' + err.message);
      response.status(500).send('Error querying the database');

      return connection.commit()
    }

    response.json({ transportation_train_trips: rows });
    connection.commit()
  });

}

export default {getAll}
