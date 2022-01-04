<?php
class Slider
{
    public $conn;

    public $sliderID = "sliderID";
    public $sliderImg = "sliderImg";
    public $productID = "productID";

    public function __construct($database)
    {
        $this->conn = $database;
    }

    public function getSlider($amount)
    {
        $query = "SELECT * FROM `slider` ORDER BY rand(" . date("Ymd") . ") LIMIT $amount;";
        $stmt = $this->conn->prepare($query);
        $stmt->setFetchMode(PDO::FETCH_ASSOC);
        $stmt->execute();

        $arraySlider = [];

        while ($row = $row = $stmt->fetch()) {
            array_push($arraySlider, array(
                $this->sliderID => $row["Sli_ID"],
                $this->sliderImg => $row["Sli_Img"],
                $this->productID => $row["Pro_ID"]
            ));
        }
        return $arraySlider;
    }
}
