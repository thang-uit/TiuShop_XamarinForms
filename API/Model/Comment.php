<?php
class Comment
{
    public $conn;

    public $commentID = "commentID";
    public $productID = "productID";
    public $userID = "userID";
    public $name = "name";
    public $rating = "rating";
    public $date = "date";
    public $content = "content";

    public function __construct($database)
    {
        $this->conn = $database;
    }

    public function GetComment($productID)
    {
        $arrayComment = [];

        $query = "SELECT * FROM `comment`, `user` WHERE `comment`.`Use_ID` = `user`.`Use_ID` AND `comment`.`Pro_ID` = '$productID' ORDER BY `Com_Date` ASC;";
        $stmt = $this->conn->prepare($query);
        $stmt->setFetchMode(PDO::FETCH_ASSOC);
        $stmt->execute();

        while ($row = $stmt->fetch()) {
            array_push($arrayComment, array(
                $this->commentID =>  $row["Com_ID"],
                $this->productID => $row["Pro_ID"],
                $this->userID => $row["Use_ID"],
                $this->name => $row["Use_Name"],
                $this->rating => $row["Com_Rating"],
                $this->date => date_format(date_create($row["Com_Date"]), "d/m/Y H:i:s"),
                $this->content => $row["Com_Content"]
            ));
        }

        return $arrayComment;
    }

    public function InsertComment($productID, $userID, $rating, $content)
    {
        $queryInsert = "INSERT INTO `comment` (`Com_ID`, `Pro_ID`, `Use_ID`, `Com_Rating`, `Com_Content`, `Com_Date`) VALUES (NULL, ?, ?, ?, ?, current_timestamp());";
        $data = array($productID, $userID, $rating, $content);
        $stmt1 = $this->conn->prepare($queryInsert);
        $stmt1->execute($data);

        if ($stmt1) {
            return 1; // Success
        } else {
            return 2; // Fail
        }
    }

    // public function UpdateCart($cartID, $quantity)
    // {
    //     $queryUpdate = "UPDATE `cart` SET `Car_Amount` = ? WHERE `cart`.`Car_ID` = ? AND `cart`.`Car_Type` = 1;";
    //     $data = array($quantity, $cartID);
    //     $stmt = $this->conn->prepare($queryUpdate);
    //     $stmt->execute($data);

    //     if ($stmt) {
    //         return 1; // Success
    //     } else {
    //         return 2; // Fail
    //     }
    // }

    // public function DeleteCart($cartID)
    // {
    //     $queryDelete = "DELETE FROM `cart` WHERE `cart`.`Car_ID` = ?;";
    //     $data = array($cartID);
    //     $stmt = $this->conn->prepare($queryDelete);
    //     $stmt->execute($data);

    //     if ($stmt) {
    //         return 1; // Success
    //     } else {
    //         return 2; // Fail
    //     }
    // }
}
