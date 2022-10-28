import re
import string

#current objective:
#Output all operations that are worth, to python, then try to reverse the operation
#    match_1 = re.search(r'if \(Math\.random\(\) < \d\.\d\)', lines[i])
#    if match_1:
#        print("Match for type 1: %s" % match_1.group())
#        pos_start = find_pos_start(i, lines)
#        pos_end = find_pos_end(lines, pos_start)
#        print("Trying to patch from %d to %d" % (pos_start, pos_end))
#        if pos_start != -1 and pos_end != -1:
#            patch_lines_type_1(lines, pos_start, pos_end)
#        
            
#    match_2 = re.search(r'if \([0-9]+n\) \{', lines[i])
#    if match_2:
#        print("Match for type 2: %s" % match_2.group())
#        pos_start = find_pos_start(i, lines)
#        pos_end = find_pos_end(lines, pos_start)
#        print("Trying to patch from %d to %d" % (pos_start, pos_end))
#        if pos_start != -1 and pos_end != -1:
#            patch_lines_type_2(lines, pos_start, pos_end)
            
#    match_3 = re.search(r'if \([0-9]+\)', lines[i])
#    if match_3:
#        pos_start = i
#        pos_end_1, pos_end_2 = find_pos_end_3(lines, pos_start)
#        print("Trying to patch from %d, %d and %d" % (pos_start, pos_end_1, pos_end_2))
#        if pos_start != -1 and pos_end != -1:
#            patch_lines_type_3(lines, pos_start, pos_end_1, pos_end_2)
#print("Reversing process for input")            
#reverse_operation(lines) 
#print_flag()

def print_flag():
    str = [chr(x) for x in flag]
    output = ",".join(str)
    print(output)


def copy_lines(in_):
    out = [""] * len(in_)
    for i in range(0, len(in_)):
        out[i] = in_[i]
    return out

regex = [r'b\[[0-9]+\] -= b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ Math\.floor\(Math\.random\(\) \* 256\);',
         r'b\[[0-9]+\] -= b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ [0-9]+;',
         r'b\[[0-9]+\] &= 0xFF;',
         r'b\[[0-9]+\] \^= \(b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ [0-9]+\) & 0xFF;',
         r'b\[[0-9]+\] \+= b\[[0-9]+\] \+ b\[2\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ [0-9]+;',
         r'b\[[0-9]+\] \+= b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ Math\.floor\(Math\.random\(\) \* 256\);'
         r'b\[[0-9]+\] \^= \(b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ Math\.floor\(Math\.random\(\) \* 256\)\) & 0xFF;'
         r'b\[[0-9]+\] \+= b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[2] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ Math\.floor\(Math\.random\(\) \* 256\);',
         r'b\[[0-9]+\] \+= b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ [0-9]+;'
         r'b\[[0-9]+\] \^= \(b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ [0-9]+\) & 0xFF;'
         r'b\[[0-9]+\] \+= b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ Math\.floor\(Math\.random\(\) \* 256\);',
         r'b\[[0-9]+\] \+= b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ [0-9]+;',
         r'b\[[0-9]+\] \^= \(b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ Math\.floor\(Math\.random\(\) \* 256\)\) & 0xFF;',
         r'b\[[0-9]+\] \+= b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ b\[[0-9]+\] \+ Math\.floor\(Math\.random\(\) \* 256\);'
        ] 
   

def modify_string(line):
    list_str = list(line)
    for i in range(0, len(list_str)):
        if list_str[i] == ')' and list_str[i + 1] == ';':
            list_str[i] = ' '
            list_str[i + 1] = ' '
        if list_str[i] == 'F' and list_str[i + 1] == ';':
            list_str[i + 1] = ' '
        if list_str[i] in string.digits and list_str[i + 1] == ';':
            list_str[i + 1] = ' '
    str = ''.join(list_str)
    if "Math.floor(Math.random()" in line:
        print("Math found, deleting")
        str = str.replace("Math.floor(Math.random() * 256"," ");
    return str
   
def replace_lines(lines, i):
    for j in range(0, len(regex)):
        match = re.search(regex[j], lines[i])
        match_math = re.search(r'Math\.floor\(Math\.random\(\) \* 256\)', lines[i])
        if match:
            str = modify_string(lines[i])
            if match_math:
                l = '          l("{0} "+Math.floor(Math.random() * 256));'.format(str.strip())
                l+='\n'
            else:
                l = '          l("{0}");'.format(str.strip())
                l+='\n'
            print("The line is %s" % l)
            lines[i] = l
            break

def replacing_sums_and_sub_and_xor(lines, pos):
    match_sub = re.search(r'-', lines[pos])
    match_p = re.search(r'\+', lines[pos])
    match_x = re.search(r'\^', lines[pos])
    if match_sub or match_p or match_x:
        print("Match found for + or - or xor")
        list_str = list(lines[pos])
        for i in range(0, len(list_str)):
            if list_str[i] == '+' and list_str[i + 1] == '=':
               print("Found sum, changing")
               list_str[i] = '-' 
            elif list_str[i] == '-' and list_str[i + 1] == '=':
               print("Found sub")
               list_str[i] = '+'    
            #elif list_str[i] == '^' and list_str[i + 1] == '=':
            #   for k in range(0, len(list_str)):
            #       list_str[k] = ''
        output_str = ''.join(list_str)
        print("Original = %s and New = %s" % (lines[pos], output_str))
        return output_str
    else:
        return lines[pos]

def reverse_operation(lines):
    updated_lines = copy_lines(lines)
    for pos in range(0, len(lines)):
        l = replacing_sums_and_sub_and_xor(lines, pos)
        if l != lines[pos]:
            lines[pos] = l
            print("Line patched to: %s" % lines[pos])
    
    file = open('dump_test_patch.js','w')
    for i in range(0, len(lines)):
        file.write(lines[i])

def patch_lines_type_1(arr_lines, pos_start, pos_end):
    first_line = pos_start
    start = re.sub(r'\} else \{', '}', arr_lines[pos_start])
    arr_lines[pos_start] = start
    for i in range(pos_start, pos_end):
        arr_lines[i] = ''
    
def find_pos_end(lines, pos_start):
    pos_end = -1
    for pos in range(pos_start, pos_start + 4, 1):
        #print("Trying to find end line in %s in pos = %d, where pos_start is %d" % (lines[pos], pos, pos_start))
        if '}' in lines[pos] and pos > pos_start:
            pos_end = pos
            break 
            
    #print("end in %d" % pos_end)
    return pos_end  

def find_pos_start(i,  lines):
    pos_start = -1
    for pos in range(i + 1, i + 4, 1):
        if '} else {' in lines[pos]:
            pos_start = pos
            break
            
    #print("start in %d" % pos_start)
    return pos_start
    

def find_pos_end_3(lines, pos_start):
    pos_end_1 = -1
    pos_end_2 = -1
    for pos in range(pos_start, pos_start + 4, 1):
        if '} else {' in lines[pos] and pos > pos_start:
            pos_end_1 = pos
            break 
    for pos in range(pos_end_1, pos_end_1 + 4, 1):
        if '}' in lines[pos] and pos > pos_end_1:
            pos_end_2 = pos
            break 

    #print("end1 and end 2 in %d and %d" % (pos_end_1, pos_end_2))
    return pos_end_1, pos_end_2


def patch_lines_type_3(arr_lines, pos_start, pos_end_1, pos_end_2):
    start = re.sub(r'if \([0-9]+\)', '', arr_lines[pos_start])
    arr_lines[pos_start] = start
    for i in range(pos_start, pos_end_1):
        arr_lines[i] = ''
    arr_lines[pos_end_1] = ''
    arr_lines[pos_end_2] = '' 
    

def patch_lines_type_2(arr_lines, pos_start, pos_end):
    first_line = pos_start
    start = re.sub(r'\} else \{', '}', arr_lines[pos_start])
    arr_lines[pos_start] = start
    for i in range(pos_start, pos_end):
        #print("Line to patch is %s " % arr_lines[i])
        arr_lines[i] = ''

def get_end_rev_1(lines, pos_start):
    pos_end = -1
    for pos in range(pos_start, pos_start + 4, 1):
        if '}' in lines[pos] and pos > pos_start:
            pos_end = pos
            break 
    return pos_end

def main():    
    lines = open('dump_copy.js', 'r').readlines()
    for i in range(0, len(lines)):
        #replace_lines(lines, i)
        l = replacing_sums_and_sub_and_xor(lines, i)
        if l != lines[i]:
            lines[i] = l
        
    file = open('dump_modified_2.js','w')
    for i in range(0, len(lines)):
        file.write(lines[i])    

if __name__ == '__main__':
	main()