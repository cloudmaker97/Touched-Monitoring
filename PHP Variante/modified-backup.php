<?php
// Setze Grundparameter
$folderpath = "."; // Pfadangabe, relativ oder absolut

$cv = new LastFileBackup($folderpath);

class LastFileBackup {
	private $folderPath = null;
	private $fileTimes = array();

	public function __construct($folder) {
		$this->folderPath = $folder;
		$this->checkTimeDiff();
	}

	public function checkTimeDiff() {
		if(!empty($this->folderPath)) {
			$fcontent = scandir($this->folderPath);
			// var_dump($fcontent);
			foreach($fcontent as $file) {
				if(!is_dir($file)) {
					$this->fileTimes[] = filemtime($this->folderPath."/".$file);
				}
			}

			asort($this->fileTimes);

			$date = new DateTime();

			if(count($this->fileTimes) > 1) {
				// Datei davor
				$date->setTimestamp($this->fileTimes[count($this->fileTimes)-2]);
				
				// Neuste Datei
				$fDate = new DateTime();
				$fDate->setTimestamp($this->fileTimes[count($this->fileTimes)-1]);
				
				$interval = date_diff($fDate, $date);
				$daysDiff = $interval->format('%a');
				if($daysDiff != 0) {
					die("Die Differenz überschreitet einen Tag");
				} else {
					die("Keine Fehler gefunden");
				}
			} else {
				die("Es ist keine Vorgängerdatei vorhanden");
			}
		} else {
			die("Ordner nicht auslesbar");
		}
	}
} 