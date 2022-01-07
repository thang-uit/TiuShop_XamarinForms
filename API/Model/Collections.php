<?php
class Collections
{
    public $conn;

    public $collectionsID = "collectionsID";
    public $name = "name";
    public $image = "image";

    public function __construct($database)
    {
        $this->conn = $database;
    }

    public function getCollections()
    {
        $query = "SELECT * FROM `collections`";
        $stmt = $this->conn->prepare($query);
        $stmt->setFetchMode(PDO::FETCH_ASSOC);
        $stmt->execute();

        $arrayCollections = [];

        while ($row = $stmt->fetch()) {
            array_push($arrayCollections, array(
                $this->collectionsID => $row["Col_ID"],
                $this->name => $row["Col_Name"],
                $this->image => $row["Col_Img"]
            ));
        }
        return $arrayCollections;
    }
}
