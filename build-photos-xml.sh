echo "<photos>"
ls -1 *.jpg | while read line ;  do     echo "<photo><filename>$line</filename><title></title><metadata><width></width><height></height></metadata><caption></caption><tags><tag></tag></tags></photo>" ; done 
echo "</photos>"