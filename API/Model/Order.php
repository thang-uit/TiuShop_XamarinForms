<?php
class Order
{
    public $conn;

    public $orderID = "orderID";
    public $usertID = "usertID";
    public $name = "name";
    public $email = "email";
    public $phone = "phone";
    public $address = "address";
    public $note = "note";
    public $payment = "payment";
    public $date = "date";
    public $dateSuccess = "dateSuccess";
    public $status = "status";
    public $total = "total";



    public function __construct($database)
    {
        $this->conn = $database;
    }

    // public function GetOrder($type, $userID)
    // {
    // }

    public function InsertOrder($userID, $name, $email, $phone, $address, $note, $total)
    {
        $queryInsert = "INSERT INTO `orders` (`Ord_ID`, `Use_ID`, `Ord_Name`, `Ord_Email`, `Ord_Phone`, `Ord_Address`, `Ord_Note`, `Ord_Payment`, `Ord_Date`, `Ord_Date_Success`, `Ord_Status`, `Ord_Total`) VALUES (NULL, ?, ?, ?, ?, ?, ?, '0', current_timestamp(), current_timestamp(), '0', ?);";
        $data = array($userID, $name, $email, $phone, $address, $note, $total);
        $stmt1 = $this->conn->prepare($queryInsert);
        $stmt1->execute($data);

        if ($stmt1) {
            return 1; // Success
        } else {
            return 2; // Fail
        }
    }

    public function InsertOrderDetail($orderID, $productID, $size, $quantity)
    {
        $queryInsert = "INSERT INTO `orderdetails` (`Ord_ID`, `Pro_ID`, `Pro_Size`, `Ode_Amount`) VALUES (?, ?, ?, ?);";
        $data = array($orderID, $productID, $size, $quantity);
        $stmt1 = $this->conn->prepare($queryInsert);
        $stmt1->execute($data);

        if ($stmt1) {
            return 1; // Success
        } else {
            return 2; // Fail
        }
    }





    private function caculateFinalPrice($price, $sale)
    {
        if ($sale != 0) {
            return $price * (100 - $sale) / 100;
        }
        return $price;
    }
}
