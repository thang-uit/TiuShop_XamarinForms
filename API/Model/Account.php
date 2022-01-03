<?php
class Account
{
    public $conn;

    public $userID;
    public $username;
    public $password;
    public $role;

    public function __construct($database)
    {
        $this->conn = $database;
    }

    // public function checkUser($username)
    // {
    //     $query = "SELECT * FROM `account` WHERE `Acc_Username` = '$username';";
    //     $stmt = $this->conn->prepare($query);
    //     $stmt->execute();
    //     return $stmt->rowCount();
    // }

    public function checkUser($username, $password)
    {
        $md5Password = md5($password); // Encoding md5 password

        $query = "SELECT * FROM `account` WHERE `Acc_Username` = '$username' AND `Acc_Password` = '$md5Password';";
        $stmt = $this->conn->prepare($query);
        $stmt->execute();
        $row = $stmt->fetch(PDO::FETCH_ASSOC);

        if ($stmt->rowCount() > 0) {
            $this->userID = $row["Acc_ID"];
            $this->username = $row["Acc_Username"];
            // $this->password = md5($row["Acc_Password"]);
            $this->role = $row["Acc_Role"];

            return 1; // Success
        } else {
            return 2; // Wrong Username or Password
        }
    }

    public function InsertUser($username, $password, $name)
    {
        $query = "SELECT * FROM `account` WHERE `Acc_Username` = '$username';";
        $stmt = $this->conn->prepare($query);
        $stmt->execute();

        if ($stmt->rowCount() <= 0) {
            $md5Password = md5($password); // Encoding md5 password

            $queryInsert = "INSERT INTO `account` (`Acc_ID`, `Acc_Username`, `Acc_Password`, `Acc_Role`) VALUES (NULL, ?, ?, '0');";
            $data = array($username, $md5Password);
            $stmt1 = $this->conn->prepare($queryInsert);
            $stmt1->execute($data);

            if ($stmt1) {
                $query1 = "SELECT * FROM `account` WHERE `Acc_Username` = '$username';";
                $stmt2 = $this->conn->prepare($query1);
                $stmt2->execute();

                if ($stmt2->rowCount() > 0) {
                    $row = $stmt2->fetch(PDO::FETCH_ASSOC);

                    $queryInsert1 = "INSERT INTO `user` (`Use_ID`, `Use_Name`, `Use_Email`, `Use_Phone`, `Use_Address`, `Use_Create`) VALUES (?, ?, NULL, NULL, NULL, current_timestamp());";
                    $data1 = array($row["Acc_ID"], $name);
                    $stmt3 = $this->conn->prepare($queryInsert1);
                    $stmt3->execute($data1);

                    $this->userID = $row["Acc_ID"];
                    $this->username = $row["Acc_Username"];
                    // $this->password = md5($row["Acc_Password"]);
                    $this->role = $row["Acc_Role"];

                    return 1; // Register success
                }
            } else {
                return 3; // Register fail
            }
        } else {
            return 2; // Username already exists
        }
    }

    public function ChangePassword($userID, $oldPassword, $newPassword)
    {
        $md5OldPassword = md5($oldPassword); // Encoding md5 old password
        $md5NewPassword = md5($newPassword); // Encoding md5 new password

        $query = "SELECT * FROM `account` WHERE `Acc_ID` = '$userID' AND `Acc_Password` = '$md5OldPassword';";
        $stmt = $this->conn->prepare($query);
        $stmt->execute();

        if ($stmt->rowCount() > 0) {
            $query1 = "UPDATE `account` SET `Acc_Password` = ? WHERE `Acc_ID` = ?;";
            $data = array($md5NewPassword, $userID);
            $stmt1 = $this->conn->prepare($query1);
            $stmt1->execute($data);

            if ($stmt1) {
                return 1; // Update password success
            } else {
                return 2; // Update password fail
            }
        } else {
            return 3; // Old password wrong
        }
    }
}
