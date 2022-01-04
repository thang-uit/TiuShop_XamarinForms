<?php
class User
{
    public $conn;

    public $username;
    public $password;
    public $role;

    public $userID;
    public $name;
    public $email;
    public $gender;
    public $phone;
    public $address;

    public function __construct($database)
    {
        $this->conn = $database;
    }

    public function getUser($userID)
    {
        $query = "SELECT * FROM `user`, `account` WHERE `user`.`Use_ID` = `account`.`Acc_ID` AND `user`.`Use_ID` = '$userID';";
        $stmt = $this->conn->prepare($query);
        $stmt->execute();
        $row = $stmt->fetch(PDO::FETCH_ASSOC);

        if ($stmt->rowCount() > 0) {
            $this->userID = $row["Use_ID"];
            $this->username = $row["Acc_Username"];
            $this->role = $row["Acc_Role"];

            $this->name = $row["Use_Name"];
            $this->email = $row["Use_Email"];
            $this->gender = $row["Use_Gender"];
            $this->phone = $row["Use_Phone"];
            $this->address = $row["Use_Address"];

            return 1; // Success
        } else {
            return 2; // Wrong userID
        }
    }

    public function updateUser($userID, $name, $email, $gender, $phone, $address)
    {
        $query = "UPDATE `user` SET `Use_Name` = ?, `Use_Email` = ?, `Use_Gender` = ?, `Use_Phone` = ?, `Use_Address` = ? WHERE `Use_ID` = '$userID';";
        $data = array($name, $email, $gender, $phone, $address);
        $stmt = $this->conn->prepare($query);
        $stmt->execute($data);

        if ($stmt) {
            return 1; // Success
        } else {
            return 2; // Update fail
        }
    }
}
